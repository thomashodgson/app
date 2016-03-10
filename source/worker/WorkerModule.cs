﻿using app_base_module;
using Autofac;
using messaging;
using RabbitMq_messaging;
using Module = Autofac.Module;

namespace worker
{
    class WorkerModule : Module
     {
         protected override void Load(ContainerBuilder builder)
         {
             base.Load(builder);
             builder.RegisterModule<BaseModule>();
             builder.RegisterModule<RabbitMqModule>();
             builder.RegisterType<HelloMessageConsumer>().As<IMessageConsumer>();
             builder.RegisterType<GoodbyeMessageConsumer>().As<IMessageConsumer>();
             builder.Register(ctx => ctx.Resolve<IMessageBusProvider>().GetMessageBus()).As<IMessageBus>();
        }
     }
 } 