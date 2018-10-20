library("robumeta")
library("metafor")
library(plyr)
library("dplyr")


# Now we visualize the meta-analysis with a forest plot. 

forest(res, xlim = c(-1.6, 1.6), atransf = transf.ztor,
	   at = transf.rtoz(c(-.4, -.2, 0, .2, .4, .6)), digits = c(2, 1), cex = .8)
text(-1.6, 18, "Author(s), Year", pos = 4, cex = .8)
text(1.6, 18, "Correlation [95% CI]", pos = 2, cex = .8)
