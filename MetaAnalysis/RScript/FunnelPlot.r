sink()
setwd()
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


filename <- 'C:\\Users\\cluni\\source\\repos\\MetaAnalysis\\MetaAnalysis\\wwwroot\\html\\funnel.png'
if (file.exists(filename)) file.remove(filename)

png(file = filename, width = 600, height = 700)
funnel(res, xlab = "Correlation coefficient")

sink(funnel.png)
dev.off()
