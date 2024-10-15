using ViewModels.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.BusinessProcessors;

namespace ProviderLayer.Processors
{
    public class BillProcessor : IProviderProcessor<BillHeader>
    {
        
        public async Task<(IEnumerable<BillHeader>, object ReturnData)> GetAll()
        {
            using BillBusiness billBusiness = new BillBusiness();
            var headerResult = await billBusiness.GetAll();
            var DetailResult = await billBusiness.GetDetails();

            var detailViewModels = from r in DetailResult.Item1
                                   select new BillDetail
                                   {
                                       ID = r.ID,
                                       HeaderID = r.HeaderID,
                                       ItemID = r.ItemID,
                                       Amount = r.Amount,
                                       Price = r.Price
                                   };

            var headerViewModels = from r in headerResult.Item1
                                   select new BillHeader
                                   {
                                       ID = r.ID,
                                       ClientID = r.ClientID,
                                       Type = r.Type,
                                       CreatedTime = r.CreatedTime,
                                       BillDate = r.BillDate,

                                       Details = detailViewModels.Where(d => d.HeaderID == r.ID)
                                   } ;

            return (headerViewModels, new int[] { (int)headerResult.ReturnData, (int)DetailResult.ReturnData});
        }

        public async Task<(bool success, object ReturnData)> Update(BillHeader parameters)
        {
            using BillBusiness billBusiness = new BillBusiness();

            var EnityDetails = new List<EntityModels.Bills.BillDetail>();
            foreach (var detail in parameters.Details)
            {
                EnityDetails.Add(new EntityModels.Bills.BillDetail
                {
                    ID = detail.ID,
                    HeaderID = detail.HeaderID,
                    ItemID = detail.ItemID,
                    Amount = detail.Amount,
                    Price = detail.Price
                });
            }

            var EntityBillHeader = new EntityModels.Bills.BillHeader
            {
                ID = parameters.ID,
                ClientID = parameters.ClientID,
                Type = parameters.Type,
                CreatedTime = parameters.CreatedTime,
                BillDate = parameters.BillDate,
                Details = EnityDetails
            };
        
            var result = await billBusiness.Update(EntityBillHeader);

            return (result.success, result.ReturnData);
        }
    
        public async Task<bool> Delete(int id)
        {
            using BillBusiness billBusiness = new BillBusiness();
            return await billBusiness.Delete(id) != 0;
        }

       
    }
}
