import os
import yt_dlp

os.system('cls' if os.name == 'nt' else 'clear')

def detect_playlist(url):
    """Check karega ke URL ek playlist hai ya sirf single video."""
    return "list=" in url

def download_youtube_video(url, quality="720p", output_folder="Downloads", start=None, end=None, is_playlist=False):
    """ YouTube se MP4 format me video aur audio H.264 codec ke sath download karega. """

    if not os.path.exists(output_folder):
        os.makedirs(output_folder)

    # 📂 Output Template Based on Playlist Detection
    filename_template = f'{output_folder}/%(playlist_index)s - %(title)s.%(ext)s' if is_playlist else f'{output_folder}/%(title)s.%(ext)s'

    # yt-dlp options
    ydl_opts = {
        'format': f'bestvideo[ext=mp4][vcodec^=avc1][height<={quality[:-1]}]+bestaudio[ext=m4a]/bestvideo[height<={quality[:-1]}]+bestaudio/best',
        'merge_output_format': 'mp4',
        'outtmpl': filename_template,
        'postprocessors': [{
            'key': 'FFmpegVideoConvertor',
            'preferedformat': 'mp4'
        }],
    }

    if is_playlist:
        if start:
            ydl_opts['playliststart'] = start
        if end:
            ydl_opts['playlistend'] = end

    with yt_dlp.YoutubeDL(ydl_opts) as ydl:
        ydl.download([url])

# 🎯 User Input
video_url = input("Enter YouTube Video or Playlist URL: ").strip()

if detect_playlist(video_url):
    print("🎥 Playlist detected!")
    start = int(input("Enter start video number: "))
    end = int(input("Enter end video number: "))
    download_youtube_video(video_url, start=start, end=end, is_playlist=True)
else:
    print("📹 Single video detected!")
    download_youtube_video(video_url, is_playlist=False)

print("✅ Download Complete! (H.264 Codec)")
