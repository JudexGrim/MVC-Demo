using ViewModels.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderLayer.Processors
{
    class BillProcessor : IProviderProcessor<BillHeader>
    {
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<BillHeader>, int maxID)> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, int ID)> Update(BillHeader parameters)
        {
            throw new NotImplementedException();
        }
    }
}
