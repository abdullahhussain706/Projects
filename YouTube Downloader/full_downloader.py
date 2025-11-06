import os
import yt_dlp
import subprocess
import json

os.system('cls' if os.name == 'nt' else 'clear')


def detect_playlist(url: str) -> bool:
    """Check karega ke URL ek playlist hai ya single video."""
    return "list=" in url


def download_youtube_video(
    url,
    quality="720p",
    output_folder="Downloads",
    start=None,
    end=None,
    is_playlist=False
):
    """YouTube se mp4 download + subtitles embed + info.json generate."""
    if not os.path.exists(output_folder):
        os.makedirs(output_folder)

    filename_template = (
        f'{output_folder}/%(playlist_index)s - %(title)s.%(ext)s'
        if is_playlist else
        f'{output_folder}/%(title)s.%(ext)s'
    )

    ydl_opts = {
        'format': f'bestvideo[ext=mp4][vcodec^=avc1][height<={quality[:-1]}]+bestaudio[ext=m4a]/best',
        'merge_output_format': 'mp4',
        'outtmpl': filename_template,

        # Subtitles (optional)
        'writesubtitles': True,
        'writeautomaticsub': True,
        'subtitleslangs': ['en'],        # ['all'] for all langs
        'subtitlesformat': 'srt',
        'embedsubtitles': True,

        # Chapters & metadata
        'writeinfojson': True,
        'addmetadata': True,

        'ignoreerrors': True,  # skip broken/private videos
    }

    if is_playlist:
        if start:
            ydl_opts['playliststart'] = start
        if end:
            ydl_opts['playlistend'] = end

    with yt_dlp.YoutubeDL(ydl_opts) as ydl:
        result = ydl.extract_info(url, download=True)

        # Playlist items ho sakte hain ya single result
        if result is None:
            print("⚠️ Download failed or no valid videos found.")
            return

        # Playlist
        if "requested_downloads" in result:
            for item in result["requested_downloads"]:
                process_chapters(item, output_folder)
        else:  # Single video
            process_chapters(result, output_folder)


def process_chapters(video_info, output_folder):
    """Chapters ko ffmpeg ke through mp4 ke andar embed karta hai."""
    if not video_info:
        return

    # --- safe title fallback ---
    title = video_info.get("title") or video_info.get("id") or "unknown"

    base = os.path.join(output_folder, title)
    mp4_file = base + ".mp4"
    json_file = base + ".info.json"

    if not os.path.exists(json_file):
        print(f"⚠️ No info.json for {title}")
        return

    with open(json_file, "r", encoding="utf-8") as f:
        data = json.load(f)

    if "chapters" not in data:
        print(f"ℹ️ No chapters for {title}")
        return

    # Create ffmetadata file
    meta_file = base + ".ffmeta"
    with open(meta_file, "w", encoding="utf-8") as f:
        f.write(";FFMETADATA1\n")
        for chap in data["chapters"]:
            start = int(chap["start_time"] * 1000)
            end   = int(chap["end_time"] * 1000)
            ctitle = chap.get("title", "Chapter")
            f.write(
                f"[CHAPTER]\nTIMEBASE=1/1000\nSTART={start}\nEND={end}\ntitle={ctitle}\n"
            )

    final_file = base + "_withChapters.mp4"

    cmd = [
        "ffmpeg", "-i", mp4_file, "-i", meta_file,
        "-map_metadata", "1", "-codec", "copy", final_file, "-y"
    ]
    subprocess.run(cmd, shell=True)
    print(f"✅ Chapters embedded → {final_file}")


# ======== RUN SCRIPT =========
video_url = input("Enter YouTube Video or Playlist URL: ").strip()

if detect_playlist(video_url):
    print("🎥 Playlist detected!")
    s = input("Start video number (or leave blank): ").strip()
    e = input("End video number (or leave blank): ").strip()
    start = int(s) if s else None
    end   = int(e) if e else None
    download_youtube_video(video_url, start=start, end=end, is_playlist=True)
else:
    print("📹 Single video detected!")
    download_youtube_video(video_url, is_playlist=False)

print("\n✅ Done! Video + (Optional) Subtitles + (Optional) Chapters embedded.")
