﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ProviderLayer.Processors
{
    public interface IProviderProcessor<T> where T: IViewModel
    {
        Task<IEnumerable<T>> GetAll();
        Task<int> Update(T parameters);
    }
}