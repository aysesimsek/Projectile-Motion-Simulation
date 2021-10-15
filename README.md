# PMS (Projectile Motion Simulation)

**Getting Started**

This study aimed to help by creating a virtual laboratory for those who want to examine the projectile motion of mechanical physics. In this way, less costly virtual laboratory simulations were created instead of creating real-life laboratory environments.

**METHOD**

In this study, a virtual projectile motion laboratory is designed. In the mechanical physics field, almost all equations are examined isolated from the real environment. Many factors are difficult to follow in the true nature of the movement. Therefore, these difficult to calculate factors are ignored and the approximate results are processed.

Another purpose of this study is to examine the neglected factors. At this point, the question "What are the neglected factors?" should be sought. The main neglected factors are air friction, gravitational acceleration, and shape of the object. Because of the earth's atmosphere, objects move in the air on the earth, not in the void. If there is no air friction all objects are fall at the same time from the higher places regardless of their shape and mass. As in the best-known example, an iron rose and a feather falls to the ground at the same time in an airless environment.

Projectile motion is the motion of an object thrown or projected into the air, subject to only the acceleration of gravity as in Figure 1.


|![image](https://user-images.githubusercontent.com/37701256/137502986-f4f0bf87-462f-4744-82b7-f6407821abd2.png)|
| :-: |
**Figure 1.** An example of projectile motion

This motion gets the information of the V0 initial velocity and the initial angle (Θ). If air friction is included in the system, some other parameters that are air density (ρ), the mass of the object (m), the dimensional constant (C), and the cross-sectional area of the projectile (A) are needed to calculate air friction force (Fd) as in Equation (1). Equation (3) and Equation (4) show that Fd’s the X-axis and Y-axis values respectively. In Equation (2), D is the drag coefficient. The weight of the mass (W) is calculated with multiply g and m such as W=gm. The negation in Equation (3) says that air friction force slows the object. Also, the negation in Equation (4) shows that projectile drops.



<img width="250" alt="Capture1" src="https://user-images.githubusercontent.com/37701256/137503516-51e432b4-04e0-45e2-9819-cf9ab2c75e8e.PNG">

Physics is all about rates of change. During the motion, the air friction affects the projectile every the smallest time interval. To examine all of these changes, Euler’s numerical method is used. Euler’s method is a simple way to approximate the solution of ordinary differential equations numerically. Assuming the Equation (5), the solution is y=f(x).


<img width="250" alt="Capture2" src="https://user-images.githubusercontent.com/37701256/137504445-f5de2077-261c-46c2-b0ec-0ab56fa7ce48.PNG">

|![image](https://user-images.githubusercontent.com/37701256/137504518-d6fdd517-a6a6-4ad3-8622-f8c36f39680e.png)|
| :-: |
|**Figure 2.** Representation of Euler’s numerical method|

If we combine the information in Figure 2 with this Equation (5), we can say that Euler’s method is numerically examining each Δx interval which means xi+1-xi from a known starting point (x0, y0) as in Equation (6). Thus, the solution is Δy=f' (x)Δx.

<img width="250" alt="Capture3" src="https://user-images.githubusercontent.com/37701256/137504923-20e1c256-c8d6-48a1-b413-e00808024438.PNG">

If we expand this equation system, we get an equation system as in Equation (7). As known, the acceleration (a) is the derivation of the velocity (V) and time (t) and the velocity is the derivation of the range (x) and time. Therefore, the system of equations in Equation (8) is obtained. Here, Δt represents the smallest possible time interval.


<img width="250" alt="Capture4" src="https://user-images.githubusercontent.com/37701256/137504953-1b8158ac-fa2a-42d7-886f-572a085420f7.PNG">

The projectile motion of an object is simple to analyze is we accept that (1) there is no air friction and (2) gravitational acceleration (g) is constant over the range of motion. When we get to this stage, we get the projectile motion as the combination of two simple shots: vertical and horizontal. The vertical component of the projectile motion is the free-fall. According to our second acceptance, the value of the gravitational acceleration is 9.8 m/s2 in order to easy calculation.

**MODEL**

The developed model offers three different data entry options. Besides, each option offers the option of whether there is air friction. There are no other forces to cease or decrease the initial force except air friction in our environment. This problem is called “projectile movement with air resistance”. In the first option, the system requires the initial velocity (V0) and the initial angle (Θ). The second option requires Vx and Vy velocities. Unlike these options, the third option requires the initial angle and point where the motion will end, which is the range (x).

The first option works on the motion if only the Vx initial velocity on the X-axis and the Vy initial velocity on the Y-axis are given as in Equation (9). The initial angle can be calculated as in Equation (10).

<img width="250" alt="Capture5" src="https://user-images.githubusercontent.com/37701256/137505196-47cb594d-cf78-467a-b312-b849feb8c0d0.PNG">

In cases of there is air friction, the developed model needs the air friction force (Fd) value. As mentioned in Chapter 2, the model needs other parameters. As it is known, the force equals the multiplication of the acceleration and the mass according to Newton’s second law (F=ma). In light of this law, the values of acceleration (a) on the X and Y axis can be calculated as ax=Fdxm and ay=Fdym. The last parameter is the time interval (Δt). Now we can find the range (x) by placing these values in Equation (8) for each time.

If there is no air friction, calculations are more simple. The range (x) of the motion is calculated using the basic range equation (x=V0xt). However, we do not know the total time of motion. Indeed, we can find total time using t=2V0yg.  The maximum height of the motion (h) can be found using h=V0yt-(gt22).

The second option is easier than the first option. Equation (10) is not needed because the initial angle and the initial speed are known. The range calculation is the same as the first option.

The third option works on when there is no air friction. Because of the nature of the projectile motion, position changes in each time interval are the same amount. Differently, when there is air friction there is no mathematical formula to solve the third option.

The developed virtual laboratory allows the user to keep track of all the motion. Whenever s/he wants, the user can pause the motion and examines outputs of the paused situation.


|![image](https://user-images.githubusercontent.com/37701256/137505269-c0ad5b4f-8d22-45b5-8633-adf845270024.png)|
| :-: |
|**Figure 3.** An example of the experiment setup|

We can see the main setup of the experiment environment in Figure 3. On the left bottom of the screen, the user can adjust the initial angle. The red box on the table is the goal of the motion. Its location is determined according to initial parameters. The user can see the range of the motion center of the table as in Figure 4.

|![image](https://user-images.githubusercontent.com/37701256/137505284-d8ee16d9-05b0-42dd-8d97-2a08e87dc14a.png)|
| :-: |
|**Figure 4.** An example of the range of a projectile motion|

The user can pause the motion whenever s/he wants and see the recent velocity and vector of the projectile as in Figure 5.

|![image](https://user-images.githubusercontent.com/37701256/137505311-ed404269-e98e-4fa0-aedc-dfbbacbe2f7e.png)|
| :-: |
|**Figure 5.** An example of the recent values of the projectile|

**CONCLUSION**

This paper introduces a virtual laboratory for mechanics physics experiments. To test the developed virtual laboratory, especially, the projectile motion problems are used. The developed system, firstly, offers to choose an approach method. The first option gets two components of initial velocity, the second option gets initial velocity itself, and the third option gets the ending point of the motion from the user.

Secondly, the system provides the user with the option of air friction. Calculations vary depending on whether air friction exists. The proposed method, Euler’s mathematical method, provides us all possible situations of the motion. Our system works very well and shows the whole nature of the movement.

The robustness of the system is measured by how accurately it gives the results. Especially when there is air friction in the system, the comparison between the results of each execution of the motion shows us if the time interval between each calculation step is narrowed, more precise results are obtained. In other words, this narrowing brings us closer to the true nature of the motion. As in Table 1, air friction changes the range of the motion. On the other hand, as we mentioned before, the initial velocity of the motion, when there is air friction, cannot be found if we have only the range information. 

**Table  SEQ Table \\* ARABIC 1**. Results of the developed system

|||**WITH AIR FRICTION**|**WITHOUT AIR FRICTION (D = 0.5)**|
| :-: | :-: | :-: | :-: |
|**1ST OPTION**|<p>V0x  = 40</p><p>V0y = 69.3</p>|Range = 174.4m|Range = 565m|
|**2ND OPTION**|<p>V0 = 80</p><p>Θ = 60</p>|Range = 174.4m|Range = 565m|
|**3RD OPTION**|<p>Range = 565m</p><p>Θ = 60</p>|-|V0 = 80|

This study proposes a model for those interested in physics to perform experiments in anywhere with a computer system. On the other hand, more user-friendly systems can be developed. Computer hardware technologies are constantly evolving. With the help of the latest improvements in virtual reality (VR) technologies, the developed model can be used with a VR device. In this way, the user can affect the motion directly in a three-dimensional virtual environment.


