using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter
{

    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();

    private static void OnListenerAdding(EventType eventType, Delegate callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))//判断事件码是否存在于事件表中
        {
            m_EventTable.Add(eventType, null);//如果不存在 则添加
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())//判断要添加的委托类型是否和委托一致
        {
            throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托,当前时间所对应的委托是{1},要添加的委托类型是{2}", eventType, d.GetType(), callBack.GetType()));
        }
    }
    private static void OnListenerRemove(EventType eventType, Delegate callBack)
    {
        if (m_EventTable.ContainsKey(eventType))//安全校验
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)//如果事件码没有对应的委托
            {
                throw new Exception(string.Format("移除监听错误:事件{0}没有对饮的委托", eventType));
            }
            else if (d.GetType() != callBack.GetType())//判断类型是否一样
            {
                throw new Exception(string.Format("移除监听错误:尝试为事件{0}移除不同类型的委托,当前委托类型为{1},要溢出的委托类型是{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除监听错误:没有事件码{0}", eventType));
        }
    }
    private static void OnlistenerRemoved(EventType eventType)
    {
        if (m_EventTable[eventType] == null)//如果事件表为空则移除
        {
            m_EventTable.Remove(eventType);
        }
    }
    public static void AddListener(EventType eventType, CallBack callBack)//添加监听
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;//关联两个委托
    }
    public static void AddListener<T>(EventType eventType, CallBack<T> callBack)//带一个参数的委托
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;//关联两个委托
    }
    public static void AddListener<T, X>(EventType eventType, CallBack<T, X> callBack)//带两个参数的委托
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X>)m_EventTable[eventType] + callBack;//关联两个委托
    }
    public static void RemoveListener(EventType eventType, CallBack callBack)//移除监听方法
    {
        OnListenerRemove(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;//移除监听
        OnlistenerRemoved(eventType);
    }
    public static void RemoveListener<T>(EventType eventType, CallBack<T> callBack)//带一个参数的移除监听方法
    {
        OnListenerRemove(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] - callBack;//移除监听
        OnlistenerRemoved(eventType);
    }
    public static void RemoveListener<T, X>(EventType eventType, CallBack<T, X> callBack)//带两个参数的移除监听方法
    {
        OnListenerRemove(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X>)m_EventTable[eventType] - callBack;//移除监听
        OnlistenerRemoved(eventType);
    }
    public static void Broadcast(EventType eventType)//广播监听
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))//判断是否获取成功
        {
            CallBack callBack = d as CallBack;//把d强转成
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同类型", eventType));
            }
        }
    }
    public static void Broadcast<T>(EventType eventType, T arg)//带一个参数的广播监听
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))//判断是否获取成功
        {
            CallBack<T> callBack = d as CallBack<T>;//把d强转成
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同类型", eventType));
            }
        }
    }
    public static void Broadcast<T, X>(EventType eventType, T arg1, X arg2)//带两个参数的广播监听
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))//判断是否获取成功
        {
            CallBack<T, X> callBack = d as CallBack<T, X>;//把d强转成
            if (callBack != null)
            {
                callBack(arg1, arg2);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应委托具有不同类型", eventType));
            }
        }
    }
}
