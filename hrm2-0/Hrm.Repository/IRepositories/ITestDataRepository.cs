using Hrm.Repository.Entity;
using System.Collections.Generic;

namespace Hrm.Repository
{
    public partial interface ITestDataRepository
    {
        List<TestDataEntity> GetTestData();
    }
}
