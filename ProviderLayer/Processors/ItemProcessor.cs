﻿using BusinessLayer.BusinessProcessors;
using CoreLib;
using System;
using ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProviderLayer.Processors
{
    public class ItemProcessor : Disposer, IProviderProcessor<Item>
    {
        private IConfiguration _configuration;

        public ItemProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(IEnumerable<Item>, object ReturnData)> GetAll()
        {
            using (ItemBusiness Processor = new ItemBusiness(_configuration))
            {
                var queryResult = await Processor.GetAll();
                var viewItems = from r in queryResult.Item1
                                select new Item
                                {
                                    ID = r.ID,
                                    Name = r.Name,
                                    Price = r.Price
                                };

                return (viewItems, queryResult.ReturnData);
            }
        }

        public async Task<(bool success, object ReturnData)> Update(Item parameters)
        {
            using ItemBusiness Processor = new ItemBusiness(_configuration);
            var entityItem = new EntityModels.Item
                                {
                                    ID = parameters.ID,
                                    Name = parameters.Name,
                                    Price = parameters.Price
            };
            
            var result = await Processor.Update(entityItem);

            return (result.success, (int)result.ReturnData); 
        }


		public async Task<T> GetStock<T>(int id)
        {
            using ItemBusiness Processor = new ItemBusiness(_configuration);
            
                return  await Processor.GetItemStock<T>(id);
            
        }

		public async Task<bool> Delete(int id)
		{
			using ItemBusiness Processor = new ItemBusiness(_configuration);
            bool success = await Processor.Delete(id) != 0;
           return success;
		}
	}
}
