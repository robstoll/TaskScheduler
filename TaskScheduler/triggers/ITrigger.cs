﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Tutteli.TaskScheduler.Triggers
{
	public interface ITrigger
	{
		DateTime GetNextTrigger(DateTime dateTime);
	}
}
