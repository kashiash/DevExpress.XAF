﻿using System;
using System.Reactive.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using Xpand.XAF.Modules.Reactive.Extensions;

namespace Xpand.XAF.Modules.Reactive.Logger.Hub {
    public sealed class ReactiveLoggerHubModule : ReactiveModuleBase{
        public const string CategoryName = "Xpand.XAF.Modules.Reactive.Logger.Hub";

        static ReactiveLoggerHubModule(){
            TraceSource=new ReactiveTraceSource(nameof(ReactiveLoggerHubModule));
            
        }
        public ReactiveLoggerHubModule() {
            RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
            RequiredModuleTypes.Add(typeof(ReactiveLoggerModule));
        }

        public static ReactiveTraceSource TraceSource{ get; set; }

        public override void Setup(ApplicationModulesManager moduleManager){
            base.Setup(moduleManager);
            Application?.Connect()
                .TakeUntil(Application.WhenDisposed())
                .Subscribe();
        }

        public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders){
            base.ExtendModelInterfaces(extenders);
            extenders.Add<IModelReactiveLogger,IModelServerPorts>();
            
        }
    }

}