using aoclib;
var numbers = Utils.GetTrimmedInput(@"input.txt").Split("\n").Select(int.Parse).ToList();

// part 1:
// with skimage this would be straight-forward
// https://scikit-image.org/docs/dev/api/skimage.morphology.html#skimage.morphology.local_minima

// .... and so I did day 9 in Python with skimage, sorry!