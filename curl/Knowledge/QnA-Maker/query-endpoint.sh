#!/bin/bash
#
# all information for your knowledgebase is found at
# https://www.qnamaker.ai/Publish?kbId=your-kb-id
#
# replace values:
# xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx = azure resources's key
# yyyyyyyy-yyyy-yyyy-yyyy-yyyyyyyyyyyy = your kb id
# myazureresourcename = QnA Maker resource name you used in Azure
# your-question = question submitted from user


curl \
--header "Content-type: application/json" \
--header "Authorization: EndpointKey xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" \
--request POST \
--data '{"question":"your-question"}' \
https://myazureresourcename.azurewebsites.net/qnamaker/knowledgebases/yyyyyyyy-yyyy-yyyy-yyyy-yyyyyyyyyyyy/generateAnswer