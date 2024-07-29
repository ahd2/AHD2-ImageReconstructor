# [AHD2-ImageReconstructor](https://github.com/ahd2/AHD2-ImageReconstructor)

<img alt="GitHub Release" src="https://img.shields.io/github/v/release/ahd2/AHD2-ImageReconstructor?style=for-the-badge"> <img alt="GitHub Release Date" src="https://img.shields.io/github/release-date/ahd2/AHD2-ImageReconstructor?style=for-the-badge"> <img alt="GitHub License" src="https://img.shields.io/github/license/ahd2/AHD2-ImageReconstructor?style=for-the-badge">

一款处理图片的Unity编辑器工具，支持将单图片通道分离为多张图片或多图片合成一张多通道图片。

## 核心特点

* 将一张图片的各个通道拆分成单张图片。
* 将多张图片组合为一张多通道图片。
* 理论上由此可实现相同大小下，图片各通道的自由组合。

## 安装

### 推荐版本

Unity2021.3.X


### 导入

#### 通过URL导入

打开Unity Package Manager，通过URL添加。输入以下URL。

 <p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2_SimpleCartoonShading/3.png?raw=true" alt="1" style="zoom: 80%;" />
  </p>

https://github.com/ahd2/AHD2-ImageReconstructor.git

## 使用说明

### 工具打开

工具位于 菜单栏-Tools-ImageReconstructor(图片重构工具)

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/0.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

### 图片拆分

图片拆分面板，拖入或选择图片后。点击按钮。

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/1.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

选择文件保存路径确认即可。

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/2.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

会在路径生成四个通道的图片（缺失通道会被纯色占据）

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/3.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

生成的图片，默认设置为单通道图片，可根据需要自己调整。

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/4.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

### 图片合并

图片合并面板，首先将各通道贴图拖入对应位置（或者选择。）

注意！！！各通道图片需要大小相等！！！

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/5.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

如果有些通道不想用，选择下面的勾选。这个通道的值就会被选择的纯色所替换。

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/6.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

手动输入图片的宽和高，或者点击按钮自动检测图片宽高。

<p align="center">
    <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/7.png?raw=true" alt="1" style="zoom: 50%;" />
  </p>

点击合并按钮选择路径即可。

#### 注意！！

* **贴图合并使用的是各个填入贴图的A通道，所以如果你填入的贴图没有A通道，推荐先用图像拆分将图像拆分。然后再选择特定通道填入。（或者你想将图像1的R通道而不是A通道来作为图像2的某一通道，也是先将图像1拆分）**

* 如果用自己的贴图直接合并，请确保打开贴图可读写。

  <p align="center">
      <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/9.png?raw=true" alt="1" style="zoom: 50%;" />
    </p>

  否则会报如下错误：

  <p align="center">
      <img src="https://github.com/ahd2/AHD2-DocsRepo/blob/main/AHD2-ImageReconstructor/8.png?raw=true" alt="1" style="zoom: 50%;" />
    </p>

### 使用示范



