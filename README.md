# 【Unity】《第一人称射箭游戏》- Lab5 博客 
video URL: 
--- 
## 一、项目配置 
1. 项目编辑器： 2021.3.11f1c2，该版本以上的 Unity 编辑器会发生 URP 编译错误； 
2. 项目模板：3D Sample Scene(URP)，其余模版会发生 URP 编译错误；
3. Starter Assets - FirstPerson 依赖：Cinemachine，Input System；
4. My Assets: Starter Assets - FirstPerson, Classical Crossbow, Fantasy Skybox FREE, URP Tree Models, Off Screen Target Indicator;
5. Import Assets 后，检查 Packages 内的 Materials， 将显示为粉色的 Material 的 shader 更改为 Universal Render Pipeline/Lit 或其他； 
## 二、游戏要求 
1. 游戏场景（14分）  
  (1) 地形（2分）：使用地形组件，上面有山、路、草、树；（可使用第三方资源改造）；  
  (2) 天空盒（2分）：使用天空盒，天空可随 玩家位置 或 时间变化 或 按特定按键切换天空盒；   
  (3) 固定靶（2分）：使用静态物体，有一个以上固定的靶标；（注：射中后状态不会变化）；  
  (4) 运动靶（2分）：使用动画运动，有一个以上运动靶标，运动轨迹，速度使用动画控制；（注：射中后需要有效果或自然落下）；  
  (5) 射击位（2分）：地图上应标记若干射击位，仅在射击位附近或区域可以拉弓射击，每个位置有 n 次机会；  
  (6) 摄像机（2分）：使用多摄像机，制作 鸟瞰图 或 瞄准镜图 使得游戏更加易于操控；  
  (7) 声音（2分）：使用声音组件，播放背景音 与 箭射出的声效；  
3. 运动与物理与动画（8分）  
  (1) 游走（2分）：使用第一人称组件，玩家的驽弓可在地图上游走，不能碰上树和靶标等障碍；（注：建议使用 unity 官方案例）；  
  (2) 射击效果（2分）：使用 物理引擎 或 动画 或 粒子，运动靶被射中后产生适当效果；  
  (3) 碰撞与计分（2分）：使用 计分类 管理规则，在射击位射中靶标得相应分数，规则自定；（注：应具有现场修改游戏规则能力）；  
  (4) 驽弓动画（2分）：使用 动画机 与 动画融合, 实现十字驽蓄力半拉弓，然后 hold，择机 shoot；  
## 三、游戏场景实现  
#### 1. 地形：使用地形组件，上面有山、路、草、树；（可使用第三方资源改造）;  
在 Fantasy Skybox FREE 的 Demo 内的 Terrain 的基础上，进行参数的修改和草与树 (URP Tree Models) 的添加; 

<img width="958" alt="72273d1f6ecfd0e444b2fb8500ec75e" src="https://github.com/user-attachments/assets/160834f7-c0bc-483d-9105-e3cd0049d207">

#### 2. 天空盒：使用天空盒，天空可随 玩家位置 或 时间变化 或 按特定按键切换天空盒;

![image](https://github.com/user-attachments/assets/6b5ec7e7-b4e2-475e-85a7-93d1012fa3aa) 

MainCamera 内的 SkyBox 相关 component 的情况如下图所示;

![image](https://github.com/user-attachments/assets/844636d0-46fb-4bba-94cc-857d859d43c9)  

在 SkyBoxController 脚本中，每隔 10s 进行一次 skyBox.material 的切换与 light.color 的切换（切换 Directional Light 的灯光颜色以营造更好的氛围感）;

#### 3. 固定靶：使用静态物体，有一个以上固定的靶标；（注：射中后状态不会变化）；

![image](https://github.com/user-attachments/assets/8a509e79-cf5b-42c5-b745-fa3ff18613ed)

#### 4. 运动靶：使用动画运动，有一个以上运动靶标，运动轨迹，速度使用动画控制；（注：射中后需要有效果或自然落下）；

![image](https://github.com/user-attachments/assets/4386da63-d9d6-4de4-932f-82f73544a597)


#### 5. 射击位：地图上应标记若干射击位，仅在射击位附近或区域可以拉弓射击，每个位置有 n 次机会；

#### 6. 摄像机：使用多摄像机，制作 鸟瞰图 或 瞄准镜图 使得游戏更加易于操控；

#### 7. 声音：使用声音组件，播放背景音 与 箭射出的声效；

## 四、运动与物理与动画实现

#### 1. 游走：使用第一人称组件，玩家的驽弓可在地图上游走，不能碰上树和靶标等障碍；（注：建议使用 unity 官方案例）；

#### 2. 射击效果：使用 物理引擎 或 动画 或 粒子，运动靶被射中后产生适当效果；

#### 3. 碰撞与计分：使用 计分类 管理规则，在射击位射中靶标得相应分数，规则自定；（注：应具有现场修改游戏规则能力）；

#### 4. 驽弓动画：使用 动画机 与 动画融合, 实现十字驽蓄力半拉弓，然后 hold，择机 shoot；
