
library("robumeta")
library("metafor")
library("plyr")
library("dplyr")
library("R2HTML")

dat <- get(data(dat.molloy2014))
dat <- mutate(dat, study_id = 1:16) # This adds a study id column 
dat <- dat %>% select(study_id, authors:quality) # This brings the study id column to the front
dat <- escalc(measure = "ZCOR", ri = ri, ni = ni, data = dat, slab = paste(authors, year, sep = ", "))
res <- rma(yi, vi, data = dat)
res
predict(res, digits = 3, transf = transf.ztor)
confint(res) #DataFrame
b_res <- rma(yi, vi, data = dat, slab = study_id)


filename <- 'C:\\Users\\cluni\\source\\repos\\MetaAnalysis\\MetaAnalysis\\wwwroot\\html\\baujat.png'
if (file.exists(filename)) file.remove(filename)

png(file = filename, width = 600, height = 600)
baujat(b_res)

dev.off()

# While the Q-statistic and I^2 can provide evidence for heterogeneity, they do not provide information on which studies may be influencing to overall heterogeneity. If there is evidence of overall heterogeneity, construction of a Bajaut plot can illustrate studies that are contribute to overall heterogeneity and the overall result. Study IDs are used to identify studies
