library("robumeta")
library("metafor")
library(plyr)
library("dplyr")
library(R2HTML)

#loadData

dat <- get(data(dat.molloy2014))
dat <- mutate(dat, study_id = 1:16) # This adds a study id column 
dat <- dat %>% select(study_id, authors:quality) # This brings the study id column to the front

# The first step is to transform r to Z and calculate the corresponding sample variances.

dat <- escalc(measure = "ZCOR", ri = ri, ni = ni, data = dat, slab = paste(authors, year, sep = ", "))
# Now you're ready to perform the meta-analysis using a random-effects model. The following commands will print out the data and also calculates and print the confidence interval for the amount of heterogeneity (I^2).

res <- rma(yi, vi, data = dat)
res
predict(res, digits = 3, transf = transf.ztor)
confint(res) 

# The output provides important information to report the meta-analysis, let's look section-by-section at the relevant data.

# "Random-Effects Model (k = 16; tau^2 estimator: REML)" 

#This line tells us we've used a random-effects model, with 16 studies (i.e., "k") and that the degree of heterogeneity (tau^2) was calculated using a restricted maximum-likelihood estimator.

# "tau^2 (estimated amount of total heterogeneity): 0.0081 (SE = 0.0055)"

# This line indicates that tau-squared was 0.0081

# "I^2 (total heterogeneity / total variability):   61.73%"

# This line indicates that I^2 was 61.73%. In other words 62.73% of variation reflected actual differences in the population mean. The confidence interval test revealed the 95% confidence interval for this value is 25.27, 88.24.

# "Test for Heterogeneity: 
# Q(df = 15) = 38.1595, p-val = 0.0009"

#These next two lines show the Q-statistic with degrees of freedom as well as the p-value of the test. In this analysis, the p-value = 0.0009, suggesting that the included studies do not share a common effect size.

# Model Results:
#
# estimate       se     zval     pval    ci.lb    ci.ub          
#   0.1499   0.0316   4.7501   <.0001   0.0881   0.2118      *** 

# Finally, we have the model results. The "estimate" provides the estimated model coefficient (i.e., the summary effect size) with standard error("se"). The z-value is the corresponding test statistic, "pval" is the corresponding p-value, "ci.lb" the the lower bound of the confidence interval and "ci.ub" the upper bound of the confidence interval.

#pred ci.lb ci.ub  cr.lb cr.ub
#0.149 0.088 0.209 -0.037 0.325

# These two lines display the transformion of Fisher's z back to Pearson's r ("pred"), and the 95% confidence interval for r ("ci.lb" and "ci.ub") for reporting the meta-analysis.

#estimate   ci.lb   ci.ub
#tau^2    0.0081  0.0017  0.0377
#tau      0.0901  0.0412  0.1943
#I^2(%)  61.7324 25.2799 88.2451
#H^2      2.6132  1.3383  8.5071

#These four lines display estimates and 95% confience intevals for heterogeneity measures as descibed above.

