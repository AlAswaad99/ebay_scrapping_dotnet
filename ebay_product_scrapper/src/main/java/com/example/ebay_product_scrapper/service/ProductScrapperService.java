package com.example.ebay_product_scrapper.service;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.jsoup.Connection;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Service;

import com.example.ebay_product_scrapper.dto.RequestDTO;
import com.example.ebay_product_scrapper.dto.ResponseDTO;
import com.example.ebay_product_scrapper.entity.Image;
import com.example.ebay_product_scrapper.entity.Product;
import com.example.ebay_product_scrapper.repository.ImageRepository;
import com.example.ebay_product_scrapper.repository.ProductRepository;
import com.example.ebay_product_scrapper.util.URLValidator;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

@Service
public class ProductScrapperService {

    @Autowired
    private ProductRepository productRepository;

    @Autowired
    private ImageRepository imageRepository;

    @Autowired
    Environment environment;

    public ResponseDTO scrapeProductsList(RequestDTO requestDTO) {
        try {
            String url = validateURL(requestDTO.getUrl());

            if (url == null || url.isEmpty())
                return new ResponseDTO(400, "Invalid URL");

            // Connection.Response initialResponse = Jsoup.connect(url)
            //         .method(Connection.Method.GET)
            //         .userAgent(
            //                 "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36")
            //         .followRedirects(true)
            //         .execute();

            // // Retrieve the cookies from the initial response
            // Map<String, String> cookies = initialResponse.cookies();

            // System.out.println(new ObjectMapper().writeValueAsString(cookies));

            Document doc = Jsoup.connect(url)
                    .method(Connection.Method.GET)
                    .userAgent(
                            environment.getProperty("jsoup.user-agent",
                                    "PostmanRuntime/7.38.0"))
                    // .cookies(cookies) // Attach the cookies to the request
                    .followRedirects(true)
                    .get();
            Elements ulElements = doc.select("ul[class*=b-list__items_nofooter]");
            List<Product> resultList = new ArrayList<Product>();
            // String ulText = doc.text();

            // System.out.println("Text"+ ulText);
            if (ulElements != null) {
                Elements items = ulElements.select("li.s-item");

                for (Element item : items) {
                    // Extract values from each item
                    String title = item.select("h3[class*=s-item__title]").text();
                    String price = item.select("span[class*=s-item__price]").text();
                    String itemNumber = "";
                    Product existingProduct = null;
                    // String imgSrc = item.select("img[class*=s-item__image-img]").attr("src");
                    try {
                        String itemUrl = item.selectFirst("a[href^='https://www.ebay.com/itm/']").attr("href");
                        System.out.println("Item URL: " + itemUrl);
                        itemNumber = itemUrl.split("\\?")[0].replace("https://www.ebay.com/itm/", "");

                        existingProduct = productRepository.findByItemNumber(itemNumber);
                    } catch (Exception e) {
                        // TODO: handle exception
                    }

                    Product product = new Product();
                    product.setId(existingProduct != null ? existingProduct.getId() : null);
                    product.setTitle(title);
                    product.setPrice(price);
                    product.setDescription(existingProduct != null ? existingProduct.getDescription() : "");
                    product.setVideoUrl(existingProduct != null ? existingProduct.getVideoUrl() : "");
                    product.setItemNumber(itemNumber);
                    // productRepository.save(product);

                    // // Create a map to store the extracted values
                    // Map<String, String> itemMap = new HashMap<>();
                    // itemMap.put("Title", title);
                    // itemMap.put("Price", price);
                    // itemMap.put("ImageSrc", imgSrc);
                    resultList.add(product);

                    // Add the map to the result list
                    // resultList.add(itemMap);
                }
                productRepository.saveAll(resultList);

                // return resultList;
                return new ResponseDTO(201, "Successfully scrapped products list");

            } else
                return new ResponseDTO(404, "Provided url doesn't contain product list");
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseDTO(500, e.getMessage());
        }

    }

    public ResponseDTO scrapreAllProductsDetails() {
        List<Product> products = productRepository.findAllByItemNumberIsNotNullAndItemNumberIsNot("");
        for (Product product : products) {
            // if (!scrapeProductDetail(product))
            // productRepository.delete(product);
            boolean scraped = scrapeProductDetail(product);
            System.out.println(scraped);
        }
        return new ResponseDTO(201, "Successfully scrapped products details");
    }

    public ResponseDTO scrapreProductDetails(String itemNumber) {
        Product product = productRepository.findByItemNumber(itemNumber);
        if (product == null)
            return new ResponseDTO(404, "Product not found");
        if (scrapeProductDetail(product))
            return new ResponseDTO(201, "Successfully scrapped products details");
        else
            return new ResponseDTO(500, "Failed to scrape product details");
    }

    private boolean scrapeProductDetail(Product product) {
        try {
            String url = "https://www.ebay.com/itm/" + product.getItemNumber();

            Document doc = Jsoup.connect(url)
                    .userAgent("PostmanRuntime/7.38.0")
                    .get();
            Element firstDiv = doc.select("div[class^=ux-image-grid-container]:first-of-type").first();
            Elements dlElements = doc.select("dl[data-testid=ux-labels-values]");

            List<Image> imageUrls = new ArrayList<Image>();
            List<Map<String, String>> resultList = new ArrayList<>();

            // String ulText = ulElements.text();

            // System.out.println("Text"+ ulText);
            if (firstDiv != null) {
                Elements imageElements = firstDiv.select(".ux-image-grid-item img");
                int count = 0;
                for (Element img : imageElements) {
                    String imgUrl = img.attr("src");
                    System.out.println("Image URL: " + imgUrl);
                    imageUrls.add(new Image(imgUrl, product));
                    if (count == 5)
                        break;
                    count++;
                }

                imageRepository.saveAll(imageUrls);

                // return resultList;

            }
            if (dlElements != null) {
                for (Element dlElement : dlElements) {
                    Elements dtElements = dlElement.select("dt");
                    Elements ddElements = dlElement.select("dd");

                    for (int i = 0; i < dtElements.size(); i++) {
                        Element dt = dtElements.get(i);
                        Element dd = ddElements.get(i);

                        String dtText = dt.text();
                        String ddText = dd.text();

                        Map<String, String> map = new HashMap<>();
                        map.put(dtText, ddText);
                        resultList.add(map);
                    }
                }

                // Convert list of maps to JSON string
                String jsonString = null;
                try {
                    ObjectMapper objectMapper = new ObjectMapper();
                    jsonString = objectMapper.writeValueAsString(resultList);
                    product.setDescription(jsonString);
                    productRepository.save(product);
                } catch (JsonProcessingException e) {
                    e.printStackTrace();
                }
                // imageRepository.saveAll(imageUrls);

                // return resultList;

            }

            return true;

        } catch (Exception e) {
            e.printStackTrace();
            // return new ResponseDTO(500, e.getMessage());
            return false;
        }
    }

    private String validateURL(String url) {
        return URLValidator.isEbayUrl(url) ? URLValidator.sanitizeUrl(url) : "";
    }
}
