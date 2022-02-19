using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core
{
    public static class AutoMapper
    {
        private static MapperConfiguration _config;
        public static MapperConfiguration Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<AppMapperProfile>();
                    });
                }
                return _config;
            }
        }
    }
}
