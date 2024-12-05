# 【Unity】《第一人称射箭游戏》- Lab5 博客  
Video URL: https://www.bilibili.com/video/BV1BzidYnEcP/  
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
在 Fantasy Skybox FREE 的 Demo 内的 Terrain 的基础上，进行参数的修改和草与树 (URP Tree Models, 自行向 Models 内添加碰撞体) 的添加;

<img width="958" alt="72273d1f6ecfd0e444b2fb8500ec75e" src="https://github.com/user-attachments/assets/160834f7-c0bc-483d-9105-e3cd0049d207">

#### 2. 天空盒：使用天空盒，天空可随 玩家位置 或 时间变化 或 按特定按键切换天空盒;

![image](https://github.com/user-attachments/assets/6b5ec7e7-b4e2-475e-85a7-93d1012fa3aa) 

MainCamera 内的 SkyBox 相关 component 的情况如下图所示;

![image](https://github.com/user-attachments/assets/b50a80e1-2d52-432d-b2f3-17c900bcacfb)

在 SkyBoxController 脚本中，每隔 10s 进行一次 skyBox.material 的切换与 light.color 的切换（切换 Directional Light 的灯光颜色以营造更好的氛围感）;

#### 3. 固定靶：使用静态物体，有一个以上固定的靶标；（注：射中后状态不会变化）；

![image](https://github.com/user-attachments/assets/8a509e79-cf5b-42c5-b745-fa3ff18613ed)

Asset: Off Screen Target Indicator 使用方法:

a. 目标组件添加 Target 脚本；

b. 为项目内的含 CinemachineVirtualCamera 的物体添加 Extended Flycam 脚本；

c. 向 Canvas 内添加 OffScreenIndicator Panel 物体 (可直接使用 Demo 内的 Canvas)；

Asset: Off Screen Target Indicator 实现效果:

![image](https://github.com/user-attachments/assets/eb97e994-4ab8-477c-9591-785fb9fcc2bc)

#### 4. 运动靶：使用动画运动，有一个以上运动靶标，运动轨迹，速度使用动画控制；（注：射中后需要有效果或自然落下）；

![image](https://github.com/user-attachments/assets/4386da63-d9d6-4de4-932f-82f73544a597)

其中，Animation Position 脚本用于使动画系统支持相对坐标；

```cs
  public class AnimationPosition : MonoBehaviour
  {
      [HideInInspector] public Vector3 positon;
      private Vector3 startPosition;
  
      void Start()
      {
          this.startPosition = this.transform.position;
      }
  
      void Update()
      {
          Vector3 newPosition = this.startPosition + this.positon;
          if (newPosition != this.startPosition)
              this.transform.position = newPosition;
      }
  }
```

![image](https://github.com/user-attachments/assets/25ca27b0-c5ab-4413-bd08-001c3beff287)

Prefab: KineticTarget 内 Animator 组件的 Animator Controller 详情如下图所示: 

![image](https://github.com/user-attachments/assets/3d902706-6658-4d33-a412-bc708469faf2)

调整 State: TargetMove 内的 float 类型参数 Speed 的值以调节运动靶标的运动速度;

![image](https://github.com/user-attachments/assets/eabc9a18-4733-4c1b-a83f-55af4ebf8ed2)

调整 Animator Controller 内的 float 类型变量 Blend (0 / 0.5 / 1) 的值以调节运动靶标运动轨迹 (圆周运动 / 三角运动 / 矩阵运动);

#### 5. 射击位：地图上应标记若干射击位，仅在射击位附近或区域可以拉弓射击，每个位置有 n 次机会；

创建预制件 ShootArea，作为射击关卡的模板；

![image](https://github.com/user-attachments/assets/05d17af9-1a16-4654-843e-867279d3ee56)

编写 ShootAreaController 脚本，将其绑定于 ShootArea 的 Bottom 上，脚本的部分代码实现如下：

```cs
  void Update()
  {
      if(gameFinished == false)
      {
          if (running == true)
          {
              bool finished = true;
              // 判断是否所有运动靶被射中
              foreach (Transform child in TargetList.transform)
              {
                  if (child.gameObject.activeSelf == true)
                  {
                      finished = false;
                      break;
                  }
              }
              if (finished)
              {
                  gameFinished = true;
                  AirWall.SetActive(false);
                  TipText.GetComponent<Text>().text = "";
                  // 修改 Bottom 的 Material
                  gameObject.GetComponent<Renderer>().material = victory;
              }
          }
          // 按键 P 中断挑战
          if (running == true && Input.GetKeyDown(KeyCode.P))
          {
              running = false;
              AirWall.SetActive(false);
  
              foreach (Transform child in TargetList.transform)
              {
                  child.gameObject.SetActive(true);
              }
              TargetList.SetActive(false);
  
              TipText.GetComponent<Text>().text = "";
              gameObject.GetComponent<Renderer>().material = waiting;
          }
      }
  }

  // 玩家进入射击关卡，开启挑战
  private void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.CompareTag("Player") && gameFinished == false)
      {
          AirWall.SetActive(true);
          TargetList.SetActive(true);
          TipText.GetComponent<Text>().text = "按键 P 中断挑战";
          running = true;
          gameObject.GetComponent<Renderer>().material = shooting;
      }
  }
```

地图上的射击关卡布置如下图所示:

![image](https://github.com/user-attachments/assets/a7d1934a-b38e-40e8-a3c4-ed22903f348f)

#### 6. 摄像机：使用多摄像机，制作 鸟瞰图 或 瞄准镜图 使得游戏更加易于操控；

使用 Starter Assets - FirstPerson 内的 MainCamera, PlayerFollowCamera 与 PlayerCapsule；

![image](https://github.com/user-attachments/assets/90aeb3a4-bd7e-4c01-ac30-022700ecbc4e)

将 MainCamera 的 CinemachineBrain 组件内的 Default Blend 设置为 Cut, 以匹配开镜的视觉效果；

将 PlayerFollowCamera 的 CinemachineVirtualCamera 组件内的 Follow 设置为 PlayerCapsule 的子物体 PlayerCameraRoot;

选中 PlayerFollowCamera, Ctrl + D, 并将新 GameObject 命名为 ScopedCamera, 调节其 CinemachineVirtualCamera 组件内的 Lens 属性； 

编写 CameraController 脚本;

```cs
  void Update()
  {
      if (Input.GetMouseButtonDown(1))  // 鼠标右键
      {
          is_normal = !is_normal;
          normalCamera.SetActive(is_normal);
          scopedCamera.SetActive(!is_normal);
      }
  }
```

将 CameraController 脚本绑定在 MainCamera 上，瞄准后的场景效果如下图所示：

![image](https://github.com/user-attachments/assets/51a37c06-d66b-4e08-b811-374a6de488ad)

#### 7. 声音：使用声音组件，播放背景音 与 箭射出的声效；

创建含 Audio Source 组件的游戏对象 BackgroundMusic 用于播放背景音乐；

编写 CrossbowController 脚本，将其绑定在 Crossbow (Classical Crossbow 内的 Model) 上；

在 CrossbowController 脚本内, 为 Crossbow 添加 Audio Source 组件，在拉弓 (Filling) 与射箭 (Shooting) 时设置相应的 Audio Clip 并播放 Audio Source；

## 四、运动与物理与动画实现

#### 1. 游走：使用第一人称组件，玩家的驽弓可在地图上游走，不能碰上树和靶标等障碍；（注：建议使用 unity 官方案例）；

使用 Unity 官方案例 Starter Assets - FirstPerson 实现，详见上文 "三、游戏场景实现 —— 6. 摄像机"；

#### 2. 射击效果：使用 物理引擎 或 动画 或 粒子，运动靶被射中后产生适当效果；

编写 ArrowController 脚本，并将其绑定在 Arrow (Classical Crossbow 内的 Prefab) 上，并添加 OnCollisionEnter() 的碰撞判定；

```cs
  // ArrowController
  void OnCollisionEnter(Collision collision)
  {
      waitDestroy = true;
  
      // 箭射中物体后停止运动
      Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
      rigidbody.isKinematic = true;
      rigidbody.velocity = Vector3.zero;

      // 播放箭射中的音效
      if (collision.gameObject.CompareTag("StaticTarget") || collision.gameObject.CompareTag("KineticTarget"))
      {
          AudioClip audioClip = Resources.Load<AudioClip>("Audios/ShootToTarget");
          audioSource.clip = audioClip;
          audioSource.volume = 0.5f;
          audioSource.Play();
      }

      // 运动靶被射中后的效果实现
      if (collision.gameObject.CompareTag("KineticTarget"))
      {
          TargetController controller = collision.gameObject.GetComponent<TargetController>();
          controller.ShootToTarget();

          destroyTime = 1.5f;
          // 调用计分类函数
          ScoreController scoreController = ScoreController.GetInstance();
          scoreController.CalculateScore(collision.gameObject, gameObject);
      }
  }

  // TargetController
  public void ShootToTarget()
  {
      // 创建粒子系统
      GameObject particle = Instantiate(Resources.Load<GameObject>("Prefabs/ParticleSystem"));
      particle.transform.position = gameObject.transform.position;
  
      waitDestroy = true;
      // 避免射中后的运动靶的干扰
      gameObject.GetComponent<Animator>().enabled = false;
      gameObject.GetComponent<Target>().enabled = false;
      gameObject.GetComponent<MeshCollider>().enabled = false;
  }
```

Arrow 碰撞到 KineticTarget 后，运动靶停止运动，不再被 Target Indicator 标记为目标，并在停止运动的位置创建 Prefab: ParticleSystem; 1.5s 后，运动靶与创建的粒子系统一同销毁；

![image](https://github.com/user-attachments/assets/aabe4a6b-68bd-46bd-ae76-34305d733cfb)

#### 3. 碰撞与计分：使用 计分类 管理规则，在射击位射中靶标得相应分数，规则自定；（注：应具有现场修改游戏规则能力）；

添加 Player 层与 NoCollideWithPlayer 层；

![image](https://github.com/user-attachments/assets/59b74b1b-f4cf-42cc-8aeb-e9c96aca99cd)

Arrow 位于 NoCollideWithPlayer 层；

PlayerCapsule 与 ShootArea 内的 AirWall 位于 Player 层；

计分类脚本 ScoreController 的代码内容如下:

```cs
  public class ScoreController
  {
      private static ScoreController instance;
      
      private float currentScore = 0;
  
      public static ScoreController GetInstance()
      {
          if(instance == null)
          {
              instance = new ScoreController();
          }
          return instance;
      }

      // Target：被射中的运动靶, Arrow：射中运动靶的箭
      public void CalculateScore(GameObject Target, GameObject Arrow)
      {
          float distanceWithPlayer = (Camera.main.transform.position - Arrow.transform.position).magnitude;

          Vector3 attackPosition = Arrow.transform.position;
          attackPosition.z = Target.transform.position.z;  // Arrow 射中处
          // Arrow 射中处与 Target 中心的距离
          float distanceWithTargerCenter = (Target.transform.position - attackPosition).magnitude;

          // 距离 / N * 环数
          currentScore += (distanceWithPlayer / 10) * (10 - distanceWithTargerCenter / 1.5f);
          currentScore -= currentScore % 1.0f;
  
          GameObject ScoreText = GameObject.Find("ScoreText");
          ScoreText.GetComponent<Text>().text = "Scores: " + currentScore.ToString();
      }
  }
```

#### 4. 驽弓动画：使用 动画机 与 动画融合, 实现十字驽蓄力半拉弓，然后 hold，择机 shoot；

Crossbow (Classical Crossbow 内的 Model) 内 Animator 组件的 Animator Controller 详情如下图所示: 

![image](https://github.com/user-attachments/assets/1e0544c2-a515-4deb-b5ba-f3a13e129429)

取消 Transition: Filling -> Hold 内的 Has Exit Time 的勾选； 

选中 Animation: Empty (位于 Model: Crossbow 内), Ctrl + D, 并将新 Animation 命名为 Empty_New, 删除 Empty_New 内与箭相关的运动； 

![image](https://github.com/user-attachments/assets/fed804d9-8749-45af-85c7-08b878f7df30)

Crossbow 绑定的 CrossbowController 脚本的部分代码如下：

```cs
  void Update()
  {
      // 获取当前动画状态信息
      AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
  
      if (Input.GetMouseButtonUp(0))  // 鼠标左键
      {
          if (animatorStateInfo.IsName("Filling"))
          {
              animator.SetBool("Filling", false);
          }
      }
  
      if (animatorStateInfo.IsName("Empty"))
      {
          if (Input.GetMouseButtonDown(0))
          {
              Filling();
          }
      }
      else
      if (animatorStateInfo.IsName("Filling"))
      {
          blend = Mathf.Min(1.0f, animatorStateInfo.normalizedTime);
          animator.SetFloat("Blend", blend);  // 动画融合
  
          if (animatorStateInfo.normalizedTime >= 1.0f)  // 十字弩拉满
          {
              animator.SetBool("Filling", false);
              audioSource.Pause();
          }
          else
          if (Input.GetMouseButton(0) == false)  // 十字弩拉满前松开鼠标左键，半拉满
          {
              animator.SetBool("Filling", false);
              audioSource.Pause();
          }
      }
      else
      if (animatorStateInfo.IsName("Hold"))
      {
          if (Input.GetMouseButtonDown(0))
          {
              ShootArrow();
          }
      }
      else
      if (animatorStateInfo.IsName("Shoot"))  // 重置变量，以待下一次射击
      {
          if (animatorStateInfo.normalizedTime >= 1.0f)
          {
              animator.SetBool("Shooting", false);
          }
      }
  }
  
  void Filling()
  {
      arrowInModel.SetActive(true);
      animator.SetBool("Filling", true);
  
      // 播放十字弩拉弓音效
      AudioClip audioClip = Resources.Load<AudioClip>("Audios/Filling");
      audioSource.clip = audioClip;
      audioSource.pitch = 1.25f;
      audioSource.Play();
  }
  
  void ShootArrow()
  {
      animator.SetBool("Shooting", true);
      arrowInModel.SetActive(false);
  
      // 播放十字弩射击音效
      AudioClip audioClip = Resources.Load<AudioClip>("Audios/Shooting");
      audioSource.clip = audioClip;
      audioSource.pitch = 1;
      audioSource.Play();
  
      // 创建 Arrow 对象并对其施加力
      GameObject newArrow = GameObject.Instantiate(Resources.Load("Prefabs/Arrow", typeof(GameObject))) as GameObject;
      newArrow.transform.position = arrowOriginTransform.position;
      newArrow.transform.rotation = arrowOriginTransform.rotation;
  
      Rigidbody rigidbody = newArrow.GetComponent<Rigidbody>();
      rigidbody.AddForce(newArrow.transform.forward * 0.3f, ForceMode.Impulse);
  }
```
