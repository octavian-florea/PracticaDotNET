using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace UnitTestPractica.Data
{
    [TestClass]
    public class UnitTestDataBase
    {
        [TestMethod]
        public void TestQuery()
        {
            // arrange  
            DataBase dataBase = new DataBase();
            int count = 0;

            // act  
            DbDataReader dataReader = dataBase.ExecuteQuery("SELECT COUNT(*) FROM activity");
            if (dataReader.Read())
            {
                count = dataReader.GetInt16(0);
            }

            // assert  
            Assert.IsTrue(count > 0, "The query didnt return any record");
        }
    }
}
