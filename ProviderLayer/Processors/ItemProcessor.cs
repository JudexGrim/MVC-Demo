using BusinessLayer.BusinessProcessors;
using CoreLib;
using System;
using ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderLayer.Processors
{
    public class ItemProcessor : Disposer, IProviderProcessor<Item>
    {
        public async Task<(IEnumerable<Item>, int maxID)> GetAll()
        {
            using (ItemBusiness Processor = new ItemBusiness())
            {
                var queryResult = await Processor.GetAll();
                var entityItems = queryResult.Item1;
                var viewItems = from r in entityItems
                                select new Item
                                {
                                    ID = r.ID,
                                    Name = r.Name,
                                    Price = r.Price
                                };
                var maxID = queryResult.maxID;
                return (viewItems, maxID);
            }
        }

        public async Task<(bool success,int ID)> Update(Item parameters)
        {
            using ItemBusiness Processor = new ItemBusiness();
            var entityItem = new EntityModels.Item
                                {
                                    ID = parameters.ID,
                                    Name = parameters.Name,
                                    Price = parameters.Price
                                };
            
            return await Processor.Update(entityItem); 
        }


		public async Task<T> GetStock<T>(int id)
        {
            using ItemBusiness Processor = new ItemBusiness();
            
                return  await Processor.GetItemStock<T>(id);
            
        }

		public async Task<bool> Delete(int id)
		{
			using ItemBusiness Processor = new ItemBusiness();
            bool success = await Processor.Delete(id) != 0;
           return success;
		}
	}
}
