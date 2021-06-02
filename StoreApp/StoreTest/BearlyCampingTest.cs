using System;
using StoreDL;
using StoreBL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace StoreTest
{
    public class BearlyCampingTest
    {
        private readonly Mock<StoreBLInterface> _BLMock;
        public BearlyCampingTest(Mock<StoreBLInterface> mockBL)
        {
            _BLMock = mockBL;
        }

        [Fact]
        public void Test_1()
        {
            

        }
    }
}
