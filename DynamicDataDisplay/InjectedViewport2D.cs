﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Microsoft.Research.DynamicDataDisplay
{
	/// <summary>
	/// Represents a Viewport of InjectedPlotter, which has a way for InjectedPlottter to change the behavior of
	/// how Visible property is coerced in dependence on Parent plotter's Visible.
	/// </summary>
	internal sealed class InjectedViewport2D : Viewport2D
	{
		internal InjectedViewport2D(FrameworkElement host, Plotter2D plotter) : base(host, plotter) { }

		protected override DataRect CoerceVisible(DataRect newVisible)
		{
			DataRect baseValue = base.CoerceVisible(newVisible);
			if (CoerceVisibleFunc != null)
				return CoerceVisibleFunc(newVisible, baseValue);
			else
				return baseValue;
		}

		public Func<DataRect, DataRect, DataRect> CoerceVisibleFunc
		{
			get;
			set;
		}
	}
}
