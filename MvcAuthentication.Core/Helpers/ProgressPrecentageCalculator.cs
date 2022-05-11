using MvcAuthentication.Core.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuthentication.Core.Helpers
{
    public static class ProgressPrecentageCalculator
    {
        public static float Calculate(List<UnansweredQuestion> unansweredQuestions, List<LevelQuestion> totalQuestions)
        {
            return 100f - (((float)unansweredQuestions.Count / (float)totalQuestions.Count) * 100f);
        }
    }
}
