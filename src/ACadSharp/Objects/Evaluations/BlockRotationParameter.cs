using ACadSharp.Attributes;
using CSMath;
using System.Collections.Generic;

namespace ACadSharp.Objects.Evaluations
{
	public class BlockRotationParameter : Block2PtParameter
	{
		/// <inheritdoc/>
		public override string ObjectName => DxfFileToken.ObjectBlockRotationParameter;

		/// <inheritdoc/>
		public override string SubclassMarker => DxfSubclassMarker.BlockRotationParameter;

		[DxfCodeValue(171)]
		public short Value171 { get; set; }

		[DxfCodeValue(172)]
		public short Value172 { get; set; }

		[DxfCodeValue(173)]
		public short Value173 { get; set; }

		[DxfCodeValue(94)]
		public int Value94 { get; set; }

		[DxfCodeValue(303)]
		public string Value303 { get; set; }

		[DxfCodeValue(174)]
		public short NumberOfConnections { get; set; }

		[DxfCodeValue(95)]
		public int Value95 { get; set; }

		[DxfCodeValue(304)]
		public string Value304 { get; set; }

		[DxfCodeValue(177)]
		public LinearParameterBaseLocation BaseLocation { get; set; }

		public XYZ BaseAnglePoint { get; set; }

		/// <summary>
		/// Rotation parameter name.
		/// </summary>
		[DxfCodeValue(305)]
		public string Name { get; set; }

		[DxfCodeValue(306)]
		public string Description { get; set; }

		[DxfCodeValue(140)]
		public double DimensionLineOffset { get; set; }

		[DxfCodeValue(96)]
		public int Flags { get; set; }

		[DxfCodeValue(142)]
		public double Minimum { get; set; }

		[DxfCodeValue(143)]
		public double Maximum { get; set; }

		[DxfCodeValue(141)]
		public double Increment { get; set; }

		public List<double> Values { get; } = [];
	}
}