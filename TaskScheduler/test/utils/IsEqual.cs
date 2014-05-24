using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch.tutteli.taskscheduler.test.utils
{
	public class IsEqual
	{
		public static NUnit.Framework.Constraints.EqualConstraint WithinOneMillisecond(DateTime startDate)
		{
			return NUnit.Framework.Is.EqualTo(startDate).Within(1).Milliseconds;
		}
	}
}