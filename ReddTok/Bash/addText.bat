set txt=%2
set txt=%txt:"='%
ffmpeg -i %1 -vf "drawtext=text=%txt%:box=1:boxborderw=5:boxcolor=black@0.5:x=(w-text_w)/2:y=(h-text_h)/2:fontsize=25:fontcolor=white" %3 