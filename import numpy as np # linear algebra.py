import numpy as np  # linear algebra
import pandas as pd  # data processing, CSV file I/O (e.g. pd.read_csv)
import zipfile  # to handle zip files
import os

# Extract the data from the .zip file
with zipfile.ZipFile(r'C:\Users\Luan\Downloads\amazon_review_polarity_csv.tgz.zip', 'r') as zip_ref:
    zip_ref.extractall('data')

# Print paths to verify extraction
for dirname, _, filenames in os.walk('./'):
    for filename in filenames:
        print(os.path.join(dirname, filename))

# Load the training data set
train_df = pd.read_csv('./data/amazon_review_polarity_csv/train.csv', header=None)
print(train_df.head())

# Load the test data set
test_df = pd.read_csv('./data/amazon_review_polarity_csv/test.csv', header=None)
print(test_df.head())
