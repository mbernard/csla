﻿using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Csla.Serialization;
using Csla.Serialization.Mobile;
using Csla.Core.FieldManager;
using Csla.Core;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Csla.Properties;

namespace Csla.Core
{
  public class BindingList<T> : ObservableCollection<T>, IBindingList
  {
    private bool _supportsChangeNotificationCore = true;
    protected virtual bool SupportsChangeNotificationCore { get { return _supportsChangeNotificationCore; } }
    //protected bool IsReadOnlyCore { get; set; }

    #region IBindingList Members

    private bool _raiseListChangedEvents = true;
    
    public bool AllowEdit { get; set; }
    public bool AllowNew { get; set; }
    public bool AllowRemove { get; set; }
    public bool RaiseListChangedEvents { get { return _raiseListChangedEvents; } set { _raiseListChangedEvents = value; } }

    public void AddNew()
    {
      AddNewCore();
    }

    #endregion

    public event EventHandler<AddedNewEventArgs<T>> AddedNew;

    public virtual void OnAddedNew(T item)
    {
      var args = new AddedNewEventArgs<T>(item);
      if (AddedNew != null)
        AddedNew(this, args);
    }

    protected virtual void AddNewCore()
    {
      // TODO: Evaluate this resource
      throw new NotImplementedException(Resources.AddNewCoreMustBeOverriden);
    }

    protected virtual void OnCoreAdded(object sender, DataPortalResult<T> e)
    {
      Add(e.Object);
      OnAddedNew(e.Object);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (SupportsChangeNotificationCore && RaiseListChangedEvents)
        base.OnCollectionChanged(e);
    }

    #region IBindingList Members

    void IBindingList.AddNew()
    {
      AddNew();
    }

    #endregion
  }
}
