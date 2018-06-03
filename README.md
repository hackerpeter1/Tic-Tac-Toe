# Unity_homework8 IMGUI、UGUI方法血条实现
### 视频地址
http://www.56.com/u86/v_MTUwODUxODc1.html  
### 运行方法
运行IMGUI方法，禁用Ethen下的Canvas，启用main。   
运行UGUI方法，启用Ethen下的Canvas，禁用main。   
### 两种方法对比
在实现IMGUI的时候，如果有多个游戏对象，各有各的血条，当重叠的候，会出现位置错乱，解决办法就是通过使用Transform.SetSiblingIndex设置index，
然后在render的时候按顺序渲染。而且能少用世界坐标就少用，因为需要花费许多开销。因此我们使用IMGUI的方法来设置血条。   
但是在实现IMGUI的时候又遇到了跟随人物一起行走的问题，解决这个问题的方法是将任务的左边传入，在onGUI创建GUI.Slider的时候通过线性组合呈现到相应的
位置上去，这样大大减少了开销，但是又有了一个新的问题，就是只能x，y坐标变化，前后无法变化，因此我们在做2d游戏的时候可以优先考虑用UGUI来显示血条。
