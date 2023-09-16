using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FootballLeague.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public ActionTargets Targets => ActionTargets.Test;

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }
    }
}
