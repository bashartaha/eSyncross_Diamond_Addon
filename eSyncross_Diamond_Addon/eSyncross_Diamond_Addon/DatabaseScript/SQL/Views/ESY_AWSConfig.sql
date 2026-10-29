/*
    Replace the empty values before deploying this view to the SAP company database.

    AccessKeyId and SecretAccessKey may both remain empty when the SAP add-on process
    uses the AWS SDK default credential chain. SessionToken is optional.

    Restrict SELECT permission on this view because it can contain AWS credentials.
*/
CREATE OR ALTER VIEW [dbo].[ESY_AWSConfig]
AS
SELECT
    CAST('' AS NVARCHAR(255)) AS [BucketName],
    CAST('' AS NVARCHAR(100)) AS [RegionName],
    CAST('' AS NVARCHAR(255)) AS [AccessKeyId],
    CAST('' AS NVARCHAR(500)) AS [SecretAccessKey],
    CAST('' AS NVARCHAR(2000)) AS [SessionToken],
    CAST('' AS NVARCHAR(500)) AS [FolderPrefix];
