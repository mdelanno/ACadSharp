using ACadSharp.Entities;
using ACadSharp.IO;
using ACadSharp.Objects;
using CSMath;
using System.IO;
using System.Linq;
using Xunit;

namespace ACadSharp.Tests.IO;

public class DimensionAssociationTests
{
	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public void WriteAndReadDimensionAssociation(bool isDxf)
	{
		CadDocument document = new CadDocument();
		Line line = new Line(XYZ.Zero, new XYZ(300, 0, 0));
		Circle circle = new Circle { Center = new XYZ(300, 0, 0), Radius = 50 };
		document.Entities.Add(line);
		document.Entities.Add(circle);

		DimensionLinear dimension = new DimensionLinear
		{
			FirstPoint = new XYZ(250, 0, 0),
			SecondPoint = new XYZ(350, 0, 0),
			DefinitionPoint = new XYZ(350, -60, 0)
		};
		document.Entities.Add(dimension);

		DimensionAssociation association = new DimensionAssociation
		{
			Name = "ACAD_DIMASSOC",
			Dimension = dimension,
			AssociativityFlags = AssociativityFlags.FirstPointReference | AssociativityFlags.SecondPointReference,
			FirstPointRef = new DimensionAssociation.OsnapPointRef
			{
				ObjectOsnapType = ObjectOsnapType.Intersection,
				Geometry = line,
				SubentType = (SubentType)2,
				GeometryParameter = 250.0 / 300.0,
				IntersectionGeometry = circle,
				IntersectionSubType = (SubentType)2,
			},
			SecondPointRef = new DimensionAssociation.OsnapPointRef
			{
				ObjectOsnapType = ObjectOsnapType.Quadrant,
				Geometry = circle,
				SubentType = (SubentType)2,
			},
		};
		CadDictionary dictionary = dimension.CreateExtendedDictionary();
		dictionary.HardOwnerFlag = true;
		dictionary.Add(association);
		dimension.AddReactor(association);
		line.AddReactor(association);
		circle.AddReactor(association);

		MemoryStream stream = new MemoryStream();
		if (isDxf)
		{
			DxfWriter.Write(stream, document);
		}
		else
		{
			DwgWriter.Write(stream, document);
		}

		CadDocument read = isDxf
			? DxfReader.Read(new MemoryStream(stream.ToArray()))
			: DwgReader.Read(new MemoryStream(stream.ToArray()));

		Dimension readDimension = read.Entities.OfType<Dimension>().Single();
		Line readLine = read.Entities.OfType<Line>().Single();
		Circle readCircle = read.Entities.OfType<Circle>().Single();
		Assert.True(readDimension.XDictionary.TryGetEntry("ACAD_DIMASSOC", out DimensionAssociation readAssociation));
		Assert.Contains(readAssociation, readDimension.Reactors);
		Assert.Contains(readAssociation, readLine.Reactors);
		Assert.Same(readDimension, readAssociation.Dimension);
		Assert.Equal(association.AssociativityFlags, readAssociation.AssociativityFlags);

		Assert.Equal(ObjectOsnapType.Intersection, readAssociation.FirstPointRef.ObjectOsnapType);
		Assert.Same(readLine, readAssociation.FirstPointRef.Geometry);
		Assert.Same(readCircle, readAssociation.FirstPointRef.IntersectionGeometry);
		Assert.Equal(250.0 / 300.0, readAssociation.FirstPointRef.GeometryParameter, 12);
		Assert.Equal((SubentType)2, readAssociation.FirstPointRef.SubentType);

		Assert.Equal(ObjectOsnapType.Quadrant, readAssociation.SecondPointRef.ObjectOsnapType);
		Assert.Same(readCircle, readAssociation.SecondPointRef.Geometry);
		Assert.Null(readAssociation.SecondPointRef.IntersectionGeometry);
	}
}
