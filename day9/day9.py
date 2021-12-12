# switching to python for this, because mathematical morphology!
# part 1 is local minima
# part 2 is watershedding (back in '99 this was an important part of my life)

# %% load file into numpy array
import numpy as np
from pathlib import Path

from skimage.morphology import extrema
from skimage.segmentation import watershed

p = Path("input.txt")

with p.open() as f:
    lines = f.readlines()

# we pad with 9s all around
img = np.zeros((len(lines)+2, len(lines[0])+2))
img.fill(9)

for idx,line in enumerate(lines):
    for ci, c in enumerate(line.strip()):
        img[idx+1, ci+1] = int(c)

# %% part 1

fp = np.ones((3,3))
fp[0,0] = fp[0,2] = fp[2,0] = fp[2,2] = 0
minima = extrema.local_minima(img, footprint=fp)
(img[minima] + 1).sum()

# %% part 2
# ignore the edges I added
mask = np.ones(img.shape)
mask[0,:] = 0
mask[-1,:] = 0
mask[:,0] = 0
mask[:,-1] = 0
# in the examples 9 is never part of a basin
mask[img==9] = 0
# yargh bug: in contrast to its docs, watershed errors when given connectivity=footprint so 
# I can't set cross structuring element / 4-connectedness.
# It seems that fortunately with this data it still gives me the right answer.
labels = watershed(img, mask=mask)
# count the size of each watershed basin
unique, counts = np.unique(labels, return_counts=True)
sizes = [o for o in zip(unique, counts) if o[0] != 0]
sizes.sort(key = lambda uc: uc[1], reverse=True)

np.prod([o[1] for o in sizes[0:3]])
