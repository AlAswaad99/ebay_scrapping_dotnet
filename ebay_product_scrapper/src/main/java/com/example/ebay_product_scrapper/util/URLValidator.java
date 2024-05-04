package com.example.ebay_product_scrapper.util;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class URLValidator {
    public static boolean isEbayUrl(String url) {
        // Regular expression pattern to match eBay URLs
        String ebayPattern = "^(http(s)?://)?([a-zA-Z0-9-]+\\.)*ebay\\.com.*$";
        Pattern pattern = Pattern.compile(ebayPattern);
        Matcher matcher = pattern.matcher(url);
        return matcher.matches();
    }

    public static String sanitizeUrl(String url) {
        // Remove query parameters from the URL
        return url.split("\\?")[0];
    }

}
