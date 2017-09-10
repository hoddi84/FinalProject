# Add a prefix to all the files in a folder with the
# specified extension, e.g. "txt".
# Usage:
# python rename.py "fileextension" "new file prefix"
# Example:
# python rename.py prefab hhp_

import os
import sys

extension = sys.argv[1]
prefix = sys.argv[2]

for filename in os.listdir("."):
    t = filename.split('.')[1]
    if extension == t:
        os.rename(filename, prefix+filename)
