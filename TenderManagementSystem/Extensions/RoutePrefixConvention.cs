using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace TenderManagementSystem.Extensions;

public static class MvcOptionsExtensions
{
    public static void UseRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
    {
        opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));
    }

    public static void UseRoutePrefix(this MvcOptions opts, string
        prefix)
    {
        opts.UseRoutePrefix(new RouteAttribute(prefix));
    }
}
public class RoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;
    public RoutePrefixConvention(IRouteTemplateProvider route)
    {
        _routePrefix = new AttributeRouteModel(route);
    }
    public void Apply(ApplicationModel application)
    {
        foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
        {
            if (selector.AttributeRouteModel != null)
            {
                selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
            }
            else
            {
                selector.AttributeRouteModel = _routePrefix;
            }
        }
    }
}