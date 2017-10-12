using AutoMapper;
using PlayNGo.FraceMarteja.DAL.Models;
using PlayNGo.FraceMarteja.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PlayNGo.FraceMarteja.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(m => {
                m.CreateMap<Hand, HandViewModel>();
                m.CreateMap<Player, PlayerViewModel>().ReverseMap();
            });
        }
    }
}
