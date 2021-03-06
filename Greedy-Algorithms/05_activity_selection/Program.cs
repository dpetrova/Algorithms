﻿//Given a set of activities S = {a1, a2, …, an}
//Each having a start & finish time: ai = {si, fi}
//Activities are "compatible" if they don't overlap, i.e.their intervals do not intersect
//What is the maximum-size subset of compatible activities, i.e.which is the largest list of compatible activities that can be scheduled?

using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_activity_selection
{
    class Program
    {
        static void Main()
        {
            int[] startingTimes = new int[] { 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12 };
            int[] endingTimes = new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            //initialize activities
            List<Activity> activities = new List<Activity>();
            for (int i = 0; i < startingTimes.Length; i++)
            {
                activities.Add(new Activity { Start = startingTimes[i], Finish = endingTimes[i] });
            }

            //sort activities by end time
            activities = activities.OrderBy(x => x.Finish).ToList();

            //last local optimum
            var lastLocal = activities.First();
            Console.WriteLine(lastLocal.ToString());

            for (int i = 1; i < activities.Count; i++)
            {
                var currActivity = activities[i];
                if (currActivity.Start >= lastLocal.Finish)
                {
                    lastLocal = currActivity;
                    Console.WriteLine(lastLocal.ToString());
                }
            }
        }
    }

    class Activity
    {
        public int Start { get; set; }
        public int Finish { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Start, this.Finish);
        }
    }
}
