#!bin/bash

echo "----~~~~ 02 DynamoDB init ~~~~----"

echo "########### Listing Tables ###########"
aws dynamodb list-tables --endpoint-url http://localhost:4566 --profile localstack

if aws dynamodb list-tables --endpoint-url http://localhost:4566 --profile localstack | grep "cxs-version"
then
    echo 'The table exists'
else
    echo 'The table doesnt exists'
    echo "########### Creating DynamoDB Table ###########"
    aws dynamodb create-table --endpoint-url=http://localhost:4566 --table-name cxs-version-configurations --attribute-definitions AttributeName=pk,AttributeType=S AttributeName=sk,AttributeType=S --key-schema AttributeName=pk,KeyType=HASH AttributeName=sk,KeyType=RANGE --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5 --profile localstack
    aws dynamodb batch-write-item --request-items file:///docker-entrypoint-initaws.d/dynamodb-data.json --endpoint-url http://localhost:4566 --profile localstack
fi
