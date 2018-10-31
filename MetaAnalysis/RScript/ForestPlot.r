
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


filename <- 'C:\\Users\\cluni\\source\\repos\\MetaAnalysis\\MetaAnalysis\\wwwroot\\html\\forest.png'
if (file.exists(filename)) file.remove(filename)

png(file = filename, width = 600, height = 600)
forest(res, xlim = c(-1.6, 1.6), atransf = transf.ztor,
       at = transf.rtoz(c(-.4, -.2, 0, .2, .4, .6)), digits = c(2, 1), cex = .8)
text(-1.6, 18, "Author(s), Year", pos = 4, cex = .8)
text(1.6, 18, "Correlation [95% CI]", pos = 2, cex = .8)

dev.off()


# Now we visualize the meta-analysis with a forest plot. 

