using System.Collections.Generic;
using DotvvmAcademy.Services;
using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.Cache
{
    public class StepsCache : KeyValueItemCacheBase<List<IStep>>
    {
        public StepsCache()
        {
            UpdateFunc = obj =>
            {
                var alp = new AllLessonProvider();
                return alp.CreateSteps();
            };
        }

        public List<IStep> Get()
        {
            return GetValue<StepsCache>();
        }

        public void Set(List<IStep> value)
        {
            SetValue<StepsCache>(value);
        }
    }
}