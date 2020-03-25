using AutoMapper;
using AutoMapper.EntityFramework;
using AutoMapper.EquivalencyExpression;
using FManager.Data.Contexts;
using System;

namespace FManager.Web.Modules
{
    public class MapperModule
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.RecognizePostfixes("Dto");
                cfg.AddProfiles("FManager.Web");

                cfg.SetGeneratePropertyMaps<GenerateEntityFrameworkPrimaryKeyPropertyMaps<FManagerEntities>>();
                cfg.CreateMissingTypeMaps = true;
                cfg.ValidateInlineMaps = false;
                cfg.ConstructServicesUsing((Type type) => ContainerExtensions.Container?.GetInstance(type));
            });
        } 
    }
}