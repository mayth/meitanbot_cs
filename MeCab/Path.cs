/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.4
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class Path : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Path(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Path obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Node rnode {
    get {
      IntPtr cPtr = MeCabPINVOKE.Path_rnode_get(swigCPtr);
      Node ret = (cPtr == IntPtr.Zero) ? null : new Node(cPtr, false);
      if (MeCabPINVOKE.SWIGPendingException.Pending) throw MeCabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public Path rnext {
    get {
      IntPtr cPtr = MeCabPINVOKE.Path_rnext_get(swigCPtr);
      Path ret = (cPtr == IntPtr.Zero) ? null : new Path(cPtr, false);
      if (MeCabPINVOKE.SWIGPendingException.Pending) throw MeCabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public Node lnode {
    get {
      IntPtr cPtr = MeCabPINVOKE.Path_lnode_get(swigCPtr);
      Node ret = (cPtr == IntPtr.Zero) ? null : new Node(cPtr, false);
      if (MeCabPINVOKE.SWIGPendingException.Pending) throw MeCabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public Path lnext {
    get {
      IntPtr cPtr = MeCabPINVOKE.Path_lnext_get(swigCPtr);
      Path ret = (cPtr == IntPtr.Zero) ? null : new Path(cPtr, false);
      if (MeCabPINVOKE.SWIGPendingException.Pending) throw MeCabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int cost {
    get {
      int ret = MeCabPINVOKE.Path_cost_get(swigCPtr);
      if (MeCabPINVOKE.SWIGPendingException.Pending) throw MeCabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float prob {
    set {
      MeCabPINVOKE.Path_prob_set(swigCPtr, value);
      if (MeCabPINVOKE.SWIGPendingException.Pending) throw MeCabPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = MeCabPINVOKE.Path_prob_get(swigCPtr);
      if (MeCabPINVOKE.SWIGPendingException.Pending) throw MeCabPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

}