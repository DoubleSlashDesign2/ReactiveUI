// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using ReactiveUI;
using System.Reactive.Concurrency;
using System.Windows.Forms;
using Splat;

namespace ReactiveUI.Winforms
{
    public class Registrations : IWantsToRegisterStuff
    {
        public void Register(Action<Func<object>, Type> registerFunction)
        {
            registerFunction(() => new PlatformOperations(), typeof(IPlatformOperations));

            registerFunction(() => new ComponentModelTypeConverter(), typeof(IBindingTypeConverter));

            registerFunction(() => new CreatesWinformsCommandBinding(), typeof(ICreatesCommandBinding));
            registerFunction(() => new WinformsCreatesObservableForProperty(), typeof(ICreatesObservableForProperty));
            registerFunction(() => new ActivationForViewFetcher(), typeof(IActivationForViewFetcher));

            if (!ModeDetector.InUnitTestRunner()) {
                WindowsFormsSynchronizationContext.AutoInstall = true;
                RxApp.MainThreadScheduler = new WaitForDispatcherScheduler(() => new SynchronizationContextScheduler(new WindowsFormsSynchronizationContext()));
            }
        }
    }
}
