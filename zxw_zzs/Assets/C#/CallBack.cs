public delegate void CallBack();//无参委托
public delegate void CallBack<T>(T arg);//一个参数的委托
public delegate void CallBack<T, X>(T arg1, X arg2);//两个参数
public delegate void CallBack<T, X, Y>(T arg1, X arg2, Y arg3);//三个
public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);//四个
public delegate void CallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5);//五个