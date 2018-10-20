library("robumeta")
library("metafor")
library(plyr)
library("dplyr")



# While the Q-statistic and I^2 can provide evidence for heterogeneity, they do not provide information on which studies may be influencing to overall heterogeneity. If there is evidence of overall heterogeneity, construction of a Bajaut plot can illustrate studies that are contribute to overall heterogeneity and the overall result. Study IDs are used to identify studies

b_res <- rma(yi, vi, data = dat, slab = study_id) # New meta-analysis with study ID identifier  

# The next command will plot a Baujat plot.

baujat(b_res) #BaujatPlot
