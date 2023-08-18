# ITNOA

# TODO: Make sure nodejs install correctlly
# nvm list available
# nvm install 18.16.1
# nvm use 18.16.1

npm install -g "autorest"
# autorest --powershell --input-file:"../BSN.SipFirewall.Api/swagger.json" --verbose
autorest --powershell --input-file:"../BSN.IpTables.Api/swagger.json" --verbose