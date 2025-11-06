## 📥 YouTube Video Downloader

This repository contains **two Python scripts**:

| Script                   | Description                                                                                          |
| ------------------------ | ---------------------------------------------------------------------------------------------------- |
| **simple_downloader.py** | Downloads YouTube videos or playlists in MP4 (H.264) format.                                         |
| **full_downloader.py**   | Downloads videos with embedded English subtitles and chapter metadata (VLC shows timeline chapters). |

---

### ✨ Common Features

* Downloads MP4 (H.264) video + best available audio.
* Supports both single videos and playlists (with optional start & end range).
* Graceful error handling (skips unavailable videos).

---

### 🛠 Requirements

* **Python 3.8+**
* **[yt-dlp](https://github.com/yt-dlp/yt-dlp)**
* **[FFmpeg](https://ffmpeg.org/)** 

Install Python dependencies:

```bash
pip install yt-dlp
```

---

### ⚙️ FFmpeg Setup (for Full Version)

1. Download the latest Windows build:
   👉 [FFmpeg Static Builds](https://www.gyan.dev/ffmpeg/builds/)
   **Direct ZIP Example:** [ffmpeg-git-full.7z](https://www.gyan.dev/ffmpeg/builds/ffmpeg-git-full.7z)

2. Extract the ZIP file to a folder, e.g. `C:\ffmpeg`.

3. Add to **System Path**:

   * Open **Start → Environment Variables**
   * Under **System Variables**, select `Path` → **Edit** → **New** →
     Add:

     ```
     C:\ffmpeg\bin
     ```

4. Test installation:

   ```bash
   ffmpeg -version
   ```

   If you see version info, FFmpeg is correctly installed.

---

### ▶️ Usage

#### 1️⃣ **Simple Video Downloader**

```bash
python simple_downloader.py
```

Steps:

1. Enter a YouTube URL (single video or playlist).
2. The script automatically detects playlists.
3. Output: MP4 files will be saved in the `Downloads/` folder.

---

#### 2️⃣ **Full Downloader (Video + Subtitles + Chapters)**

```bash
python full_downloader.py
```

Steps:

1. Enter the YouTube URL.
2. If it’s a playlist, enter optional start and end numbers.
3. Output files:

   * `video.mp4` → includes embedded subtitles (if available)
   * `video_withChapters.mp4` → includes embedded chapters (if available)

> ⚠️ **Note:**
>
> * Subtitles and chapters will only be embedded if they exist on the YouTube video.
> * If not available, the video will still download normally.

---

### ⚙️ Script Options (Inside Code)

* `quality="720p"` → You can change to `1080p`, etc.
* `subtitleslangs = ['en']` (in full version) → use `['all']` to download all available languages.

---

### 💡 Recommended Player

* **VLC Media Player** – fully supports chapters and embedded subtitles.


You can include these files in your GitHub repository or ZIP release so users can easily choose between the **basic video downloader** and the **full subtitles + chapters version**.
