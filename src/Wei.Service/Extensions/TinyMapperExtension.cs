using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;
using System;

namespace Wei.Service
{
    public static class TinyMapperExtension
    {

        public static Target MapTo<Tsource, Target>(this Tsource obj)
        {
            if (obj == null)
            {
                return default;
            }
            else
            {
                if (!TinyMapper.BindingExists<Tsource, Target>())
                {
                    TinyMapper.Bind<Tsource, Target>();
                }
                return TinyMapper.Map<Target>(obj);
            }
        }

        public static Target MapTo<Tsource, Target>(this Tsource obj, Action<IBindingConfig<Tsource, Target>> config)
        {
            if (obj == null)
            {
                return default;
            }
            else
            {
                if (!TinyMapper.BindingExists<Tsource, Target>())
                {
                    TinyMapper.Bind(config);
                }
                return TinyMapper.Map<Target>(obj);
            }
        }
    }
}
