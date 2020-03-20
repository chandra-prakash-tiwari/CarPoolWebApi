using AutoMapper;

namespace CarPoolingWebApi.Services.Service
{
    class Mapper
    {
        private static MapperConfiguration Config { get; set; }

        public static TDesc Map<TSrc, TDesc>(TSrc data)
        {
            Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSrc, TDesc>();
            });

            return Config.CreateMapper().Map<TSrc, TDesc>(data);
        }
    }
}
