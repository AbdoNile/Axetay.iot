
$bucket_name = "axetay-iot-states";
$aws_region = "us-east-1"
aws s3api head-bucket --bucket $bucket_name
$bucket_exists = aws s3api head-bucket --bucket $bucket_name
if( -not $bucket_exists)
{

    aws s3api create-bucket --bucket $bucket_name --region $aws_region  ;

}
else{
    Write-Host "$bucket_name exsits!"
}
Remove-Item .terraform -Force -Recurse
terraform init -backend-config="bucket=$bucket_name"
