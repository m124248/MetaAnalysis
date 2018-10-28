library("robumeta")
library("metafor")
library("plyr")
library("dplyr")
library("R2HTML")
#args <- commandArgs()
#metaAnalysisFilepath <- args[2]


filename <- 'C:\\Users\\cluni\\source\\repos\\MetaAnalysis\\MetaAnalysis\\wwwroot\\html\\funnel.png'
if (file.exists(filename)) file.remove(filename)
#dir.create(dirname(filename), showWarnings = FALSE)

png(file = filename, width = 500, height = 900)
funnel(res, xlab = "Correlation coefficient")
dev.off()

#wd <- getwd()
#imagedir <- paste(Sys.getenv("HOME"), "C:/MetaAnalysis/MetaAnalysis/wwwroot/html/", sep = "")
#setwd(imagedir)



#setwd(wd)