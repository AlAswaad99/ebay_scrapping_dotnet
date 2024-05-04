package com.example.ebay_product_scrapper.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class RequestDTO {

    @JsonProperty("Url")
    private String url;

    @JsonProperty("ItemNumber")
    private String itemNumber;
}
