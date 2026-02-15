我将把 `TodayIsland` 项目移植到 **Avalonia UI**，并确保与 `ClassIsland-jydev` 宿主环境兼容。

### 1. 项目配置调整 (`TodayIsland.csproj`)

* **目标框架**：更新为 `net8.0`（移除 `net8.0-windows`）。

* **依赖项**：

  * 移除 `UseWPF` 和 `MaterialDesignThemes.Wpf`。

  * 无需手动添加 `Avalonia` 包，SDK 会自动传递这些依赖。

### 2. XAML 界面迁移 (`.xaml` -> `.axaml`)

我将把所有视图文件（如 `NextHolidayDate`、`WeekOddToday` 等）转换为 Avalonia 格式：

* **文件扩展名**：将 `.xaml` 重命名为 `.axaml`。

* **命名空间**：

  * 将 `xmlns` 更新为 `https://github.com/avaloniaui`。

  * 保留 SDK 提供的 `xmlns:ci="http://classisland.tech/schemas/xaml/core"`。

* **图标替换**：

  * 将 `MaterialDesign` 图标（如 `<material:PackIcon Kind="Calendar" />`）替换为 SDK 内置的 `LucideIcon`，以符合新版宿主风格。

### 3. 代码迁移 (C#)

* **命名空间**：将 `System.Windows.*` 替换为 `Avalonia.*` 和 `Avalonia.Controls`。

* **线程处理**：将 `Dispatcher.Invoke(...)` 更新为 `Dispatcher.UIThread.InvokeAsync(...)`（使用 `Avalonia.Threading`）。

* **设置访问**：保留 `((dynamic)AppBase.Current).Settings` 的动态访问方式，因为运行时 `App` 实例（在 `ClassIsland` 中）仍然包含 `Settings` 属性。

### 4. 验证

* 尝试构建项目，确保所有本地引用和 Avalonia 类型解析正确。

* 确保代码隐藏文件（Code-behind）正确关联到新的 `.axaml` 文件。

