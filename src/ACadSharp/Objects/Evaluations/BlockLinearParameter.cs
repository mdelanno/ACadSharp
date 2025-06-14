using ACadSharp.Attributes;
using System;

namespace ACadSharp.Objects.Evaluations
{
	/// <summary>
	/// 
	/// </summary>
	[DxfName(DxfFileToken.ObjectBlockLinearParameter)]
	[DxfSubClass(DxfSubclassMarker.BlockLinearParameter)]
	public class BlockLinearParameter : Block2PtParameter
	{
		/// <inheritdoc/>
		public override ObjectType ObjectType => ObjectType.UNLISTED;

		/// <inheritdoc/>
		public override string ObjectName => DxfFileToken.ObjectBlockLinearParameter;

		/// <inheritdoc/>
		public override string SubclassMarker => DxfSubclassMarker.BlockLinearParameter;

		/// <summary>
		/// Label text.
		/// </summary>
		[DxfCodeValue(305)]
		public string Label { get; set; }

		[DxfCodeValue(306)]
		public string Description { get; set; }

		[DxfCodeValue(140)]
		public double ActualDistance { get; set; }

		[DxfCodeValue(141)]
		public double Increment { get; set; }

		[DxfCodeValue(142)]
		public double Minimum { get; set; }

		[DxfCodeValue(143)]
		public double Maximum { get; set; }

		[DxfCodeValue(170)]
		public DistanceType DistanceType { get; set; }
	}

	[Flags]
	public enum DistanceType
	{
		NoConstraint = 0,

		Increment = 1,

		ValuesList = 2
	}
}